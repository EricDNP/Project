using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PracticePackFront.Helpers;
using PracticePackFront.Models;
using PracticePackFront.Attributes;

namespace PracticePackFront.Controllers
{
    [RedirectingAction]
    public class PackageController : Controller
    {
        const string Route = "Packages";
        private readonly PracticePackAPI _api = new PracticePackAPI();

        // GET: PackageController
        public async Task<ActionResult> Index()
        {
            User identity = (User)ViewData["Identity"];
            IEnumerable<Package> packages = null;

            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/filter/{identity.Id}");
                if (result.IsSuccessStatusCode)
                {
                    packages = await result.Content.ReadFromJsonAsync<IList<Package>>();
                }
                else
                {
                    packages = Enumerable.Empty<Package>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View(packages);
        }

        public async Task<Package> GetById(Guid id)
        {
            Package package = null;
            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    package = await result.Content.ReadFromJsonAsync<Package>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return package;
        }

        // GET: PackageController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            Package package = await GetById(id);
            return View(package);
        }

        // GET: PackageController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PackageController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Package package)
        {
            User user = (User)ViewData["Identity"];

            package.Id = new Guid();
            package.UserId = user.Id;
            package.Status = "Ongoing";

            using (var client = _api.Initial())
            {
                var postTask = client.PostAsJsonAsync<Package>(Route, package);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return View(package);
        }

        // GET: PackageController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            Package package = await GetById(id);
            return View(package);
        }

        // POST: PackageController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Package package)
        {
            if (ModelState.IsValid)
            {
                using (var client = _api.Initial())
                {
                    var response = await client.PutAsJsonAsync<Package>($"{Route}/{id}", package);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Errror");
                    }
                }
            }
            return View(package);
        }

        // GET: PackageController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: PackageController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            using (var client = _api.Initial())
            {
                var result = await client.DeleteAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View();
        }
    }
}
