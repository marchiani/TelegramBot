using CodeariumBot.Extention;
using CodeariumServices.Settings;
using CodeariumServices.Implementations;
using CodeariumServices.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace CodeariumBot
{
	public class Startup
	{
		private readonly IConfiguration Configuration;
		private TelegramBotConfiguration TelegrabBotSettings => Configuration.GetSection("TelegrabBotSettings").Get<TelegramBotConfiguration>();
		private AzureStorageSettings AzureStorageSettigns => Configuration.GetSection("AzureStorageSettings").Get<AzureStorageSettings>();


		public Startup(IWebHostEnvironment env)
		{
			IConfigurationBuilder builder = new ConfigurationBuilder()
						.SetBasePath(env.ContentRootPath)
						.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
						.AddEnvironmentVariables();

			Configuration = builder.Build();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<IInitialBotService, InitialBotService>();

			services.AddSingleton(TelegrabBotSettings);
			services.AddSingleton(AzureStorageSettigns);

			services
				.AddTelegramBotClient(TelegrabBotSettings)
				.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
					options.SerializerSettings.ContractResolver = new DefaultContractResolver();
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
