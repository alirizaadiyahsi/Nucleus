<template>
    <v-content>
        <v-container fluid fill-height>
            <v-layout align-center justify-center>
                <v-flex xs12 sm8 md4>
                    <router-view></router-view>
                    <v-layout class="mt-3" align-center justify-center>
                        <v-menu>
                            <template v-slot:activator="{ on }">
                                <v-btn color="primary" dark outlined rounded v-on="on">
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
                    </v-layout>
                </v-flex>
            </v-layout>
        </v-container>
    </v-content>
</template>

<script src="./account-layout.ts"></script>