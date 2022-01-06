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
    public class CardController : Controller
    {
        const string Route = "Cards";
        private readonly PracticePackAPI _api = new PracticePackAPI();

        // GET: CardController
        public async Task<ActionResult> Index()
        {
            User identity = (User)ViewData["Identity"];
            IEnumerable<Card> cards = null;

            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/filter/{identity.Id}");
                if (result.IsSuccessStatusCode)
                {
                    cards = await result.Content.ReadFromJsonAsync<IList<Card>>();
                }
                else
                {
                    cards = Enumerable.Empty<Card>();
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }

            return View(cards);
        }

        public async Task<Card> GetById(Guid id)
        {
            Card card = null;
            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    card = await result.Content.ReadFromJsonAsync<Card>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return card;
        }

        // GET: CardController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            Card card = await GetById(id);
            return View(card);
        }

        // GET: CardController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CardController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Card card)
        {
            User identity = (User)ViewData["Identity"];

            card.Id = new Guid();
            card.UserId = identity.Id;

            if(ModelState.IsValid)
            {
                using (var client = _api.Initial())
                {
                    var postTask = client.PostAsJsonAsync<Card>(Route, card);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error");
                    }
                }
            }
            return View(card);
        }

        // GET: CardController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            Card card = await GetById(id);
            return View(card);
        }

        // POST: CardController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, Card card)
        {
            User identity = (User)ViewData["Identity"];
            card.UserId = identity.Id;

            if (ModelState.IsValid)
            {
                using (var client = _api.Initial())
                {
                    var response = await client.PutAsJsonAsync<Card>($"{Route}/{id}", card);
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
            return View(card);
        }

        // GET: CardController/Delete/5
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
            return RedirectToAction(nameof(Index));
        }

        // POST: CardController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
