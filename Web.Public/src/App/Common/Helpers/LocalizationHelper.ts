import { getLocaleFromNavigator, init, register } from 'svelte-i18n';
import { Config } from 'App';

export async function RegisterLocalLangs(){
    register("en", () => import('../../../assets/en.json'));
    register("es", () => import('../../../assets/es.json'));
    init({
        fallbackLocale: 'en',
        initialLocale: getLocaleFromNavigator(),
      });

}
export async function RegisterFromAPI(langId : string,endpoint : string){
    
    register(langId,() => import(/* @vite-ignore */ `${Config.ApiURl}${endpoint}`));     
}