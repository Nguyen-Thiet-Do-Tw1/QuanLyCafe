using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var role = context.Session.GetString("UserRole");

        if (role == "Admin" && !context.Request.Path.Value.Contains("/Admin"))
        {
            context.Response.Redirect("/Admin");
            return;
        }

        if (role == "NhanVien" && !context.Request.Path.Value.Contains("/Staff"))
        {
            context.Response.Redirect("/Staff");
            return;
        }

        if (role == "KhachHang" && !context.Request.Path.Value.Contains("/User"))
        {
            context.Response.Redirect("/User");
            return;
        }

        await _next(context);
    }
}
