<template>
    <v-app-bar color="blue darken-3" dense fixed clipped-left app dark>
        <v-app-bar-nav-icon @click="drawerChanged"></v-app-bar-nav-icon>
        <v-toolbar-title>
            <span class="title">Nucleus Vue</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-menu>
            <template v-slot:activator="{ on }">
                <v-btn color="primary" v-on="on">
                    <img :src="require('@/assets/images/icons/flags/' + selectedLanguage.languageCode + '.png')" class="mr-2 ml-1" />
                    {{selectedLanguage.languageName}}
                    <v-icon dark class="ml-3">mdi-menu-down</v-icon>
                </v-btn>
            </template>

            <v-list>
                <v-list-item @click="changeLanguage('en', 'English')">
                    <img src="@/assets/images/icons/flags/en.png" class="mr-2" />
                    <v-list-item-title>English</v-list-item-title>
                </v-list-item>
                <v-list-item @click="changeLanguage('tr', 'Türkçe')">
                    <img src="@/assets/images/icons/flags/tr.png" class="mr-2" />
                    <v-list-item-title>Türkçe</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-menu>
        <v-menu class="ml-3">
            <template v-slot:activator="{ on }">
                <v-btn color="primary" v-on="on">
                    {{authStore.getTokenData().sub}}
                    <v-icon dark class="ml-3">mdi-menu-down</v-icon>
                </v-btn>
            </template>

            <v-list>
                <v-list-item @click="changePasswordDialogChanged(true)">
                    <v-icon>mdi-lock</v-icon>
                    <v-list-item-title>{{$t('ChangePassword')}}</v-list-item-title>
                </v-list-item>
            </v-list>
        </v-menu>

        <v-btn icon @click="logOut">
            <v-icon>mdi-logout-variant</v-icon>
        </v-btn>

        <change-password :logOut="logOut"></change-password>
    </v-app-bar>
</template>

<script src="./top-menu.ts"></script>