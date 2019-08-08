export default class LanguageStore {
    public static storageKey: string = 'language';

    public static getLanguage(): ILanguageDto {
        return JSON.parse(localStorage.getItem(LanguageStore.storageKey) as string) ||
            ({ languageCode: 'en', languageName: 'English' } as ILanguageDto);
    }

    public static setLanguage(input: ILanguageDto) {
        localStorage.setItem(LanguageStore.storageKey, JSON.stringify(input));
    }

    public static removeLanguage(): void {
        localStorage.removeItem(LanguageStore.storageKey);
    }
}