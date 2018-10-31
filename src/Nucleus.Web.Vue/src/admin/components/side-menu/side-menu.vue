<template>
    <v-navigation-drawer v-model="drawer" fixed clipped app>
        <v-list dense>
            <v-list-tile v-for="item in mainMenuItems" :key="item.link" :to="item.link">
                <v-list-tile-action>
                    <v-icon>{{ item.icon }}</v-icon>
                </v-list-tile-action>
                <v-list-tile-content>
                    <v-list-tile-title>
                        {{ item.text }}
                    </v-list-tile-title>
                </v-list-tile-content>
            </v-list-tile>
            <v-subheader class="mt-3 grey--text text--darken-1">ADMINISTRATION</v-subheader>
            <v-list>
                <v-list-tile v-for="item in adminMenuItems" :key="item.link" :to="item.link">
                    <v-list-tile-action>
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-tile-action>
                    <v-list-tile-content>
                        <v-list-tile-title>
                            {{ item.text }}
                        </v-list-tile-title>
                    </v-list-tile-content>
                </v-list-tile>
            </v-list>
        </v-list>
    </v-navigation-drawer>
</template>

<style src="./side-menu.css"></style>
<script src="./side-menu.ts"></script>