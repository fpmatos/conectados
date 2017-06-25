

var _window: any = window;

console.log('sou config prod');

export const environment = {
  production: true,
  authenticateUrl: `${_window.config.apiEndpoint}/token`,
  rootApiUrl: `${_window.config.apiEndpoint}/api`,
  uploadUrl: `${_window.config.apiEndpoint}/api/Uploads/SalvarArquivo`,
  appUrl: _window.config.appUrl
};
