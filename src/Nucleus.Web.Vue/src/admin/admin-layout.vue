<template>
    <div>
        <aside-menu>
        </aside-menu>
        <top-menu>
        </top-menu>
        <v-content>
            <v-container>
                <router-view></router-view>
            </v-container>
        </v-content>
        <v-footer app fixed dark>
            <span>&copy; Version: {{nucleus.appVersion}}</span>
        </v-footer>
    </div>
</template>

<script src="./admin-layout.ts"></script>