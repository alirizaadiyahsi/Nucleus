# Nucleus

Web API startup template with a Vue Client demo.

## How to Start?

- Select `Nucleus.Web.Api` project "**Set as Startup Project**" 
- Open "**Package Manager Console**" and select default project as `src/Nucleus.EntityFramework`
- Run `update-database` command to create database.
- Run(F5 or CTRL+F5) Web API project first 
- Run `yarn` command at location `src/Nucleus.Web.Vue` to install npm packages.
- Run `yarn serve` command to run Vue application.
- Admin user name and password : `admin/123qwe`

## Vue Application Screenshots

### Login Page

<img src="_images/_login.png" alt="Vue Client Demo" class="img-thumbnail" />

### Register Page

<img src="_images/_register.png" alt="Vue Client Demo" class="img-thumbnail" />

### List Pages

<img src="_images/_users.png" alt="Vue Client Demo" class="img-thumbnail" />

### Create/Edit Pages

<img src="_images/_addUser.png" alt="Vue Client Demo" class="img-thumbnail" />

## ASP.NET Core Web API

### Project solution:

<img src="_images/project-solution.png" alt="Project Solution" class="img-thumbnail" />

### Swagger UI

<img src="_images/swagger-ui.png" alt="Swagger UI" class="img-thumbnail" />

# Document

## Adding New Language

- Add json file to store language keys and values to `Nucleus\src\Nucleus.Web.Vue\src\assets\js\locales\tr.json`
- Copy `en.json` content and translate the values to target language.
- Add country flag to `Nucleus.Web.Vue\src\assets\images\icons\flags\tr.png`. Get images from http://www.iconarchive.com/show/flag-icons-by-gosquared.html
- Add language to language selection menu in `Nucleus\src\Nucleus.Web.Vue\src\account\account-layout.vue` and `Nucleus\src\Nucleus.Web.Vue\src\admin\components\menu\top-menu\top-menu.vue` like following

**account-layout.vue**

````html
<v-layout align-center justify-center row fill-height>
    <v-menu class="mt-3">
        <v-btn slot="activator" color="primary" dark outline round>
            <img :src="require('@/assets/images/icons/flags/' + selectedLanguage.languageCode + '.png')" class="mr-2 ml-1" />
            {{selectedLanguage.languageName}}
            <v-icon dark class="ml-3">arrow_drop_down</v-icon>
        </v-btn>
        <v-list>
            <v-list-tile @click="changeLanguage('en', 'English')">
                <img src="@/assets/images/icons/flags/en.png" class="mr-2" />
                <v-list-tile-title>English</v-list-tile-title>
            </v-list-tile>
            <v-list-tile @click="changeLanguage('tr', 'Türkçe')">
                <img src="@/assets/images/icons/flags/tr.png" class="mr-2" />
                <v-list-tile-title>Türkçe</v-list-tile-title>
            </v-list-tile>
        </v-list>
    </v-menu>
</v-layout>
````

**top-menu.vue**

````html
<v-menu>
    <v-btn slot="activator" color="primary">
        <img :src="require('@/assets/images/icons/flags/' + selectedLanguage.languageCode + '.png')" class="mr-2" />
        {{selectedLanguage.languageName}}
        <v-icon dark class="ml-3">arrow_drop_down</v-icon>
    </v-btn>
    <v-list>
        <v-list-tile @click="changeLanguage('en', 'English')">
            <img src="@/assets/images/icons/flags/en.png" class="mr-2" />
            <v-list-tile-title>English</v-list-tile-title>
        </v-list-tile>
        <v-list-tile @click="changeLanguage('tr', 'Türkçe')">
            <img src="@/assets/images/icons/flags/tr.png" class="mr-2" />
            <v-list-tile-title>Türkçe</v-list-tile-title>
        </v-list-tile>
    </v-list>
</v-menu>
````

###

### Tags & Technologies

- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-2.1)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity?view=aspnetcore-2.1)
- [JWT (Bearer Token) Based Authentication](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/)
- [Automapper](https://automapper.org/)
- [Serilog](https://serilog.net/)
- [Swagger](https://swagger.io/)
- [ASP.NET Core Test Host](https://www.nuget.org/packages/Microsoft.AspNetCore.TestHost)
- [Authorization & Authentication](https://docs.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-2.1)
- [Exception Handling & Logging](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling?view=aspnetcore-2.1)
- [Microsoft.CodeCoverage](https://docs.microsoft.com/en-us/visualstudio/test/using-code-coverage-to-determine-how-much-code-is-being-tested?view=vs-2017)
- [Vue.js](https://vuejs.org/)
- [Vue Router](https://router.vuejs.org/)
- [Vuetify](https://vuetifyjs.com/en/)
- [Vue i18n](https://kazupon.github.io/vue-i18n/)
