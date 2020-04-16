using CodeariumServices.Interfaces;
using CodeariumServices.Settings;
using System;
using System.Linq;
using Microsoft.Azure.CognitiveServices.Vision;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using System.Threading.Tasks;
using System.IO;

namespace CodeariumServices.Implementations
{
	public class FaceRecognition : IFaceRecognition
	{
		private readonly IFaceClient FaceClient;
		private readonly AzureStorageSettings AzureStorageSettings;

		public FaceRecognition(AzureStorageSettings azureStorageSettings)
		{
			AzureStorageSettings = azureStorageSettings;

			FaceClient = new FaceClient(
			new ApiKeyServiceClientCredentials(AzureStorageSettings.Key),
			new System.Net.Http.DelegatingHandler[] { });
		}

		public async Task SimpleFaceRecognation()
		{
			string personGroupId = "myfriends";
			await FaceClient.PersonGroup.CreateAsync(personGroupId, "My Friends");

			var friend1 = await FaceClient.PersonGroupPerson.CreateAsync(
				personGroupId,
				"Anna"
			);

			foreach (string imagePath in Directory.GetFiles("", "*.jpg"))
			{
				using (Stream s = File.OpenRead(imagePath))
				{
					await FaceClient.PersonGroupPerson.AddFaceFromStreamAsync(
						personGroupId, friend1.PersonId, s);
				}
			}
			await FaceClient.PersonGroup.TrainAsync(personGroupId);
		}
		
		public async Task Identifire()
		{
			string testImageFile = @"D:\Pictures\test_img1.jpg";

			using (Stream s = File.OpenRead(testImageFile))
			{
				var faces = await FaceClient.Face.DetectWithStreamAsync(s);
				var faceIds = faces.Select(face => face.FaceId.Value).ToArray();

				var results = await FaceClient.Face.IdentifyAsync(faceIds, "myfriends");
				foreach (var identifyResult in results)
				{
					Console.WriteLine("Result of face: {0}", identifyResult.FaceId);
					if (identifyResult.Candidates.Count == 0)
					{
						Console.WriteLine("No one identified");
					}
					else
					{
						// Get top 1 among all candidates returned
						var candidateId = identifyResult.Candidates[0].PersonId;
						var person = await FaceClient.PersonGroupPerson.GetAsync("myfriends", candidateId);
						Console.WriteLine("Identified as {0}", person.Name);
					}
				}
			}
		}
	}
}
