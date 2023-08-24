using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using System.Linq;


namespace Blog.Controllers
{
    public class BlogController : Controller
    {

        //List in memory acting like our database
        static List<BlogEntry> Posts = new List<BlogEntry>();

        /*action method to go to the Index view, passing in the Posts data
         * bc we want to send that over to the Index page*/
        public IActionResult Index()
        {
            return View("Index", Posts);
        }


        /*give this the same name as the CreatorPage view that we made
          so that we can just use View() as the return method
        for the CreatPage method*/
        /*CreatorPage() is an ACTION, actions are a type of METHOD
          in controller classes*/
        public IActionResult CreatorPage(Guid id)
        {
            if(id != Guid.Empty)
            {
                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == id);

                return View(model: existingEntry);
            }

            return View();//this takes us to CreatorPage view since we named the action CreatorPage
        }

        //the CreatorPage post action gets the content from the froala editor as an html string
        [HttpPost]
        public IActionResult CreatorPage(BlogEntry entry)
        {
            if(entry.Id == Guid.Empty)
            {
                // for new blog article
                BlogEntry newEntry = new BlogEntry();
                newEntry.Content = entry.Content;
                newEntry.Id = Guid.NewGuid();
                Posts.Add(newEntry);//add content from the html editor to our Posts list we made at the top

            } else
            {
                // for existing blog article
                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == entry.Id);
                existingEntry.Content = entry.Content;
            }


            return RedirectToAction("Index");//takes us to the Index view, we want to go there after hitting submit button
        }



    }
}
