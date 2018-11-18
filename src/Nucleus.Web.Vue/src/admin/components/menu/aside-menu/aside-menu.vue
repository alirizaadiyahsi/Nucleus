<template>
    <v-navigation-drawer v-model="drawer" fixed clipped app dark>
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
            <v-subheader v-show="isAdmin" class="mt-3 grey--text text--darken-1">{{$t('Administration').toUpperCase()}}</v-subheader>
            <v-list v-show="isAdmin">
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

<script src="./aside-menu.ts"></script>