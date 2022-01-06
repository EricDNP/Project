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
    public class BranchController : Controller
    {
        const string Route = "Branches";
        private readonly PracticePackAPI _api = new PracticePackAPI();

        // GET: BranchController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Branch> branches = null;

            using (var client = _api.Initial())
            {
                var result = await client.GetAsync(Route);
                if (result.IsSuccessStatusCode)
                {
                    branches = await result.Content.ReadFromJsonAsync<IList<Branch>>();
                }
                else
                {
                    branches = Enumerable.Empty<Branch>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View(branches);
        }

        public async Task<Branch> GetById(Guid id)
        {
            Branch branch = null;
            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    branch = await result.Content.ReadFromJsonAsync<Branch>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return branch;
        }

        // GET: BranchController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            Branch branch = await GetById(id);
            return View(branch);
        }

        // GET: BranchController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Branch branch)
        {
            branch.Id = new Guid();

            using (var client = _api.Initial())
            {
                var postTask = client.PostAsJsonAsync<Branch>(Route, branch);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return View(branch);
        }

        // GET: BranchController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            Branch branch = await GetById(id);
            return View(branch);
        }

        // POST: BranchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Branch branch)
        {
            if (ModelState.IsValid)
            {
                using (var client = _api.Initial())
                {
                    var response = await client.PutAsJsonAsync<Branch>($"{Route}/{id}", branch);
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
            return View(branch);
        }

        // GET: BranchController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BranchController/Delete/5
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
