namespace Nancy.Demo.Hosting.Owin
{
    using System.Linq;
    using System.Threading;

    using Models;

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = x =>
                {
                    var model = new Index() { Name = "Boss Hawg" };

                    return View["Index", model];
                };

            Post["/"] = x =>
                {
                    var model = new Index() { Name = "Boss Hawg" };

                    model.Posted = this.Request.Form.posted.HasValue ? this.Request.Form.posted.Value : "Nothing :-(";

                    return View["Index", model];
                };

            Get["/fileupload"] = x =>
            {
                var model = new Index() { Name = "Boss Hawg" };

                return View["FileUpload", model];
            };

            Post["/fileupload"] = x =>
            {
                var model = new Index() { Name = "Boss Hawg" };

                var file = this.Request.Files.FirstOrDefault();
                string fileDetails = "None";

                if (file != null)
                {
                    fileDetails = string.Format("{0} ({1}) {2}bytes", file.Name, file.ContentType, file.Value.Length);
                }

                model.Posted = fileDetails;

                return View["FileUpload", model];
            };

            Get["/slowTask"] = x =>
            {
                return new AsyncResponse(() =>
                    {
                        Thread.Sleep(5000);
                        return "Done!";
                    });
            };

        }
    }
}