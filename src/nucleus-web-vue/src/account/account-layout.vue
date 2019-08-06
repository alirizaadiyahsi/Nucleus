<template>
    <v-content>
        <v-container fluid fill-height>
            <v-layout align-center justify-center>
                <v-flex xs12 sm8 md4>
                    <router-view></router-view>
                    <v-layout align-center justify-center row fill-height>
                        <v-menu class="mt-3">
                            <v-btn v-slot:activator="{ on }" color="primary" dark outlined rounded>
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
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>

<script src="./account-layout.ts"></script>