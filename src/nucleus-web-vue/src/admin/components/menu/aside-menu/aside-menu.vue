<template>
    <v-navigation-drawer v-model="drawer" fixed clipped app dark>
        <v-list dense>
            <v-list-item v-for="item in mainMenuItems" :key="item.link" :to="item.link">
                <v-list-item-action>
                    <v-icon>{{ item.icon }}</v-icon>
                </v-list-item-action>
                <v-list-item-content>
                    <v-list-item-title>
                        {{ item.text }}
                    </v-list-item-title>
                </v-list-item-content>
            </v-list-item>
            <v-subheader v-if="nucleus.auth.isGranted('Permissions_Administration')" class="mt-3 grey--text text--darken-1">{{$t('Administration').toUpperCase()}}</v-subheader>
            <v-list v-if="nucleus.auth.isGranted('Permissions_Administration')">
                <v-list-item v-for="item in adminMenuItems" :key="item.link" :to="item.link">
                    <v-list-item-action>
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-item-action>
                    <v-list-item-content>
                        <v-list-item-title>
                            {{ item.text }}
                        </v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </v-list>
        </v-list>
    </v-navigation-drawer>
</template>

<script src="./aside-menu.ts"></script>