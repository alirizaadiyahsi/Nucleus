<template>
    <v-content>
        <v-container fluid fill-height>
            <v-layout align-center justify-center>
                <v-flex xs12 sm8 md4>
                    <router-view></router-view>
                    <v-layout align-center justify-center row fill-height>
                        <v-menu class="mt-3">
                            <v-btn slot="activator" color="primary" dark outline round>
                                {{selectedLanguageName}}
                                <v-icon dark class="ml-3">arrow_drop_down</v-icon>
                            </v-btn>

                            <v-list>
                                <v-list-tile @click="changeLanguage('en', 'English')">
                                    <v-list-tile-title>English</v-list-tile-title>
                                </v-list-tile>
                                <v-list-tile @click="changeLanguage('tr', 'Türkçe')">
                                    <v-list-tile-title>Türkçe</v-list-tile-title>
                                </v-list-tile>
                            </v-list>
                        </v-menu>
                    </v-layout>
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>

<script src="./account-layout.ts"></script>