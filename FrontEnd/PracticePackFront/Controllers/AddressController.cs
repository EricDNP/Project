using Microsoft.AspNetCore.Mvc;
using PracticePackFront.Helpers;
using PracticePackFront.Models;
using PracticePackFront.Attributes;

namespace PracticePackFront.Controllers
{
    [RedirectingAction]
    public class AddressController : Controller
    {
        const string Route = "Addresses";
        private readonly PracticePackAPI _api = new PracticePackAPI();

        // GET: AddressController
        public async Task<ActionResult> Index()
        {
            User identity = (User)ViewData["Identity"];
            IEnumerable<Address> addresses = null;

            using(var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/filter/{identity.Id}");
                if(result.IsSuccessStatusCode)
                {
                    addresses = await result.Content.ReadFromJsonAsync<IList<Address>>();
                }
                else
                {
                    addresses = Enumerable.Empty<Address>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View(addresses);
        }

        public async Task<Address> GetById(Guid id)
        {
            Address address = null;
            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    address = await result.Content.ReadFromJsonAsync<Address>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return address;
        }

        // GET: AddressController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            Address address = await GetById(id);
            return View(address);
        }

        // GET: AddressController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Address address)
        {
            User identity = (User)ViewData["Identity"];

            address.Id = new Guid();
            address.UserId = identity.Id;

            using(var client = _api.Initial())
            {
                var postTask = client.PostAsJsonAsync<Address>(Route, address);
                postTask.Wait();

                var result = postTask.Result;
                if(result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ModelState.AddModelError(string.Empty, "Server Error");
            return View(address);
        }

        // GET: AddressController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            Address address = await GetById(id);
            return View(address);
        }

        // POST: AddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Address address)
        {
            if(ModelState.IsValid)
            {
                using(var client = _api.Initial())
                {
                    var response = await client.PutAsJsonAsync<Address>($"{Route}/{id}", address);
                    if(response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Errror");
                    }
                }
            }
            return View(address);
        }

        // GET: AddressController/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteAsync(Guid id)
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

        // POST: AddressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
