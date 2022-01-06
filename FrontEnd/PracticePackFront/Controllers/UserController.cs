using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using PracticePackFront.Attributes;
using PracticePackFront.Helpers;
using PracticePackFront.Models;

namespace PracticePackFront.Controllers
{
    [RedirectingAction]
    public class UserController : Controller
    {
        const string Route = "Users";
        private readonly PracticePackAPI _api = new PracticePackAPI();

        //GET : LOGIN
        public ActionResult Login()
        {
            return View();
        }

        protected async Task<List<Branch>> GetBranches()
        {
            IEnumerable<Branch> branches = null;

            using (var client = _api.Initial())
            {
                var result = await client.GetAsync("Branches");
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
            return (List<Branch>)branches;
        }

        //POST: LOGIN
        [HttpPost]
        public async Task<ActionResult> Login(User security)
        {
            ModelState[nameof(security.ConfirmPassword)].ValidationState = ModelValidationState.Valid;
            ModelState[nameof(security.Name)].ValidationState = ModelValidationState.Valid;
            ModelState[nameof(security.LastName)].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                using (var client = _api.Initial())
                {
                    var response = await client.PostAsJsonAsync<User>($"{Route}/Login", security);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (content != "")
                        {
                            User user = JsonConvert.DeserializeObject<User>(content);
                            var identity = ByteConverter.ObjectToByteArray(user);
                            HttpContext.Session.Set("Identity", identity);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Login Incorrecto");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Errror");
                    }
                }
            }
            return View(security);
        }

        public ActionResult Logout()
        {
            ViewData["Identity"] = null;
            HttpContext.Session.Set("Identity", ByteConverter.ObjectToByteArray(null));
            return RedirectToAction("Login");
        }

        //GET: REGISTER
        public ActionResult Register()
        {
            return View();
        }

        //POST: REGISTER
        [HttpPost]
        public ActionResult Register(User user)
        {
            user.Id = new Guid();
            user.BranchId = Guid.Empty;

            using (var client = _api.Initial())
            {
                var postTask = client.PostAsJsonAsync<User>(Route, user);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error");
            return View(user);
        }

        // GET: UserController
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            User user = (User)ViewData["Identity"];
            List<Branch> branches = await GetBranches();
            ViewData["Branches"] = branches;

            user.Branch = branches.FirstOrDefault(b => b.Id == user.BranchId);

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsync(Guid id, User user)
        {
            User oldUser = (User)ViewData["Identity"];

            if(user.Password == null && user.Password == null)
            {
                user.Password = oldUser.Password;
                user.ConfirmPassword = oldUser.ConfirmPassword;
                ModelState[nameof(user.Password)].ValidationState = ModelValidationState.Valid;
                ModelState[nameof(user.ConfirmPassword)].ValidationState = ModelValidationState.Valid;
            }

            if (user.BranchId == Guid.Empty)
                ModelState[nameof(user.BranchId)].ValidationState = ModelValidationState.Valid;

            user.Packages = oldUser.Packages;
            user.Cards = oldUser.Cards;
            user.Adresses = oldUser.Adresses;


            ViewData["Branches"] = await GetBranches();
            if (ModelState.IsValid)
            {
                using (var client = _api.Initial())
                {
                    var response = await client.PutAsJsonAsync<User>($"{Route}/{id}", user);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewData["Identity"] = user;
                        var identity = ByteConverter.ObjectToByteArray(user);
                        HttpContext.Session.Set("Identity", identity);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Errror");
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<User> GetById(Guid id)
        {
            User user = null;
            using (var client = _api.Initial())
            {
                var result = await client.GetAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    user = await result.Content.ReadFromJsonAsync<User>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return user;
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            User user = await GetById(id);
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            return View();
        }

        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            User user = await GetById(id);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, User user)
        {
            return RedirectToAction(nameof(IndexAsync));
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            using (var client = _api.Initial())
            {
                var result = await client.DeleteAsync($"{Route}/{id}");

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Logout));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error");
                }
            }
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Logout));
        }
    }
}
