import { getLocaleFromNavigator, init, register } from 'svelte-i18n';
import { Config } from '../Config';

export async function RegisterLocalLangs(){
    register("en", () => import('../../../Assets/Dictionaries/en.json'));
    register("es", () => import('../../../Assets/Dictionaries/es.json'));
    init({
        fallbackLocale: 'en',
        initialLocale: getLocaleFromNavigator(),
      });

}
export async function RegisterFromAPI(langId : string,endpoint : string){
    register(langId,() => import(`${Config.ApiURl}${endpoint}`));     
}