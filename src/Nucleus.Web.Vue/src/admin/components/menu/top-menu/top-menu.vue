<template>
    <v-toolbar color="blue darken-3" dense fixed clipped-left app dark>
        <v-toolbar-side-icon @click="drawerChanged"></v-toolbar-side-icon>
        <v-toolbar-title>
            <span class="title">Nucleus Vue</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-menu>
            <v-toolbar-title slot="activator">
                <span>{{authStore.getTokenData().sub}}</span>
                <v-icon dark>arrow_drop_down</v-icon>
            </v-toolbar-title>

            <v-list>
                <v-list-tile @click="changePasswordDialogChanged(true)">
                    <v-icon>lock</v-icon>
                    <v-list-tile-title>Change password</v-list-tile-title>
                </v-list-tile>
            </v-list>
        </v-menu>
        <v-menu>
            <v-toolbar-title slot="activator">
                <span>{{selectedLanguageName}}</span>
                <v-icon dark>arrow_drop_down</v-icon>
            </v-toolbar-title>

            <v-list>
                <v-list-tile @click="changeLanguage('en', 'English')">
                    <v-list-tile-title>English</v-list-tile-title>
                </v-list-tile>
                <v-list-tile @click="changeLanguage('tr', 'Türkçe')">
                    <v-list-tile-title>Türkçe</v-list-tile-title>
                </v-list-tile>
            </v-list>
        </v-menu>
        <v-btn icon v-on:click.native="logOut">
            <v-icon>exit_to_app</v-icon>
        </v-btn>

        <change-password :logOut="logOut"></change-password>
    </v-toolbar>
</template>

<script src="./top-menu.ts"></script>