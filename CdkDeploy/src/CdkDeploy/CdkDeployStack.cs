using Amazon.CDK;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.APIGateway;
using Constructs;

namespace CdkDeploy
{
    public class CdkDeployStack : Stack
    {
        internal CdkDeployStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // The code that defines your stack goes here

            var lambdaFunction = new Function(this, "AspNetCoreLambda9", new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                Handler = "WebApi::WebApi.LambdaEntryPoint::FunctionHandlerAsync",
                Code = Code.FromAsset("../WebApi/src/WebApi/bin/Release/net8.0"),
                MemorySize = 512,
                Timeout = Duration.Seconds(30)
            });

            new LambdaRestApi(this, "AspNetCoreApiGateway9", new LambdaRestApiProps
            {
                Handler = lambdaFunction,
                Proxy = true,

                //DefaultCorsPreflightOptions = new CorsOptions
                //{
                //    AllowOrigins = new[] { "*" }, // or specify domains like "https://www.example.com"
                //    AllowMethods = new[] { "GET", "POST", "OPTIONS" },
                //    AllowHeaders = new[] { "Content-Type", "X-Amz-Date", "Authorization", "X-Api-Key" }
                //}

            });
        }
    }
}
