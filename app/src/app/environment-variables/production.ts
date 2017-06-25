declare var window: any;

export const prodVariables = {
  apiEndpoint: `${window.config.apiServer}/api/`,
  authenticateUrl: `${window.config.apiServer}/token`,
  googleAnalyticsId: window.config.googleAnalyticsId,
  environmentName: 'Development Environment',
  ionicEnvName: 'dev',
  cmsServer: window.config.cmsServer,
  production: true
};
