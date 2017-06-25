// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
console.log('sou config dev');
export const environment = {
  production: false,
  authenticateUrl: 'http://localhost:58957/token',
  rootApiUrl: 'http://localhost:58957/api',
  uploadUrl: 'http://localhost:58957/api/Uploads/SalvarArquivo',
  appUrl: 'http://localhost:8100'
};
