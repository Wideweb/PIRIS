const IdentityApi: string = 'http://localhost:57603';

export const Urls = {
    signUp: '/Account/Register',
    login: '/Account/Login',
    logOff: '/Account/LogOff',
    client: IdentityApi + '/api/Client',
    clientForm: IdentityApi + '/api/Client/GetClientForm',
    
    countryLookups: IdentityApi + '/api/Lookup/GetCountryLookups',
}