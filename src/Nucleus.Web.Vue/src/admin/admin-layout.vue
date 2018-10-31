<template>
    <v-app id="inspire" dark>
        <side-menu-component />
        <top-menu-component />
        <v-content>
            <v-container>
                <router-view></router-view>
            </v-container>
        </v-content>
    </v-app>
</template>

<script src="./admin-layout.ts"></script>