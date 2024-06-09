namespace Multicast.Startup;

public static class ApiVersioning
{
    public static void AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddMvc()
            .AddApiExplorer(
                options =>
                    {
                        options.GroupNameFormat = "'v'VVV";
                    });
    }
}