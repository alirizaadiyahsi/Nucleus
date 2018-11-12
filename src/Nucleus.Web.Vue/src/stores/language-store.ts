export default class LanguageStore {
    public static storageKey: string = 'language';

    public static getLanguage(): ILanguageDto {
        return JSON.parse(localStorage.getItem(LanguageStore.storageKey) as string) || { language: 'en', languageName: 'English' };
    }

    public static setLanguage(input: ILanguageDto) {
        localStorage.setItem(LanguageStore.storageKey, JSON.stringify(input));
    }

    public static removeLanguage(): void {
        window.localStorage.removeItem(LanguageStore.storageKey);
    }
}