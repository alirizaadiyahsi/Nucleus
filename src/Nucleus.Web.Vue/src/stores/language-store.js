export default class LanguageStore {
    static getLanguage() {
        return JSON.parse(localStorage.getItem(LanguageStore.storageKey)) ||
            { languageCode: 'en', languageName: 'English' };
    }
    static setLanguage(input) {
        localStorage.setItem(LanguageStore.storageKey, JSON.stringify(input));
    }
    static removeLanguage() {
        localStorage.removeItem(LanguageStore.storageKey);
    }
}
LanguageStore.storageKey = 'language';
//# sourceMappingURL=language-store.js.map