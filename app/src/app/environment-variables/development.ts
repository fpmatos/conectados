///var apiServer = "http://8d2a98c1.ngrok.io";
var apiServer = "http://localhost:58957";
var cmsServer = "http://localhost:4200";
var googleAnalyticsId = "UA-100887695-1";

export const devVariables = {
  apiEndpoint: `${apiServer}/api/`,
  authenticateUrl: `${apiServer}/token`,
  googleAnalyticsId: googleAnalyticsId,
  environmentName: 'Local Environment',
  ionicEnvName: 'local',
  cmsServer: cmsServer,
  production: false

};
