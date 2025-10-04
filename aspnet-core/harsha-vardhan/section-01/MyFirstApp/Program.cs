var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(async (HttpContext context) =>
{


    /*
    Set the response content type and status code
    context.Response.Headers.ContentType = "text/plain";
    context.Response.Headers["Content-Type"] = "text/plain";
    context.Response.ContentType = "text/plain";
    */
    context.Response.ContentType = "text/plain";
    /*
    Set the response status code
    context.Response.StatusCode = 200;
    context.Response.StatusCode = StatusCodes.Status200OK;
    context.Response.Headers["Status-Message"] = "OK";
    */
    context.Response.StatusCode = StatusCodes.Status200OK;
    await context.Response.WriteAsync("Hello, World!");
});

app.Run();
