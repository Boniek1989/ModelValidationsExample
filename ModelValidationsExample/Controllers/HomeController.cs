using Microsoft.AspNetCore.Mvc;
using ModelValidationsExample.CustomModelBinder;
using ModelValidationsExample.Models;

namespace ModelValidationsExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("register")]
        //[Bind(nameof(Person.PersonName), nameof(Person.email), nameof(Person.Password), nameof(Person.ConfirmPassword))] Only binded properties are received
        public IActionResult Index([FromBody] [ModelBinder(BinderType = typeof(PersonModelBinder))] Person person)
        {
            if (ModelState.IsValid==false)
            {
                string errors = string.Join("\n",
                ModelState.Values.SelectMany(value => // outer loop
                value.Errors).Select(err => // inter loop
                err.ErrorMessage));
                //foreach (var value in ModelState.Values)
                //{ 
                //    foreach (var error in value.Errors)
                //    {
                //        errorsList.Add(error.ErrorMessage);
                //    }
                //}
                //string errors = string.Join("\n", errorsList); 
                return BadRequest(errors);
            }
            return Content($"{person}");
        }
    }
}
