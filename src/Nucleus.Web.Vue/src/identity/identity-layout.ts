import {defineComponent, ref} from "vue";
import LanguageStore from "@/core/stores/language-store";

export default defineComponent({
    name: "IdentityLayoutComponent",
    setup() {
        const selectedLanguage = ref(LanguageStore.getLanguage());
        const languages = ref([
            {name: 'English', code: 'en'},
            {name: 'Türkçe', code: 'tr'}
        ]);
        
        return {
            selectedLanguage,
            languages
        };
    }
});