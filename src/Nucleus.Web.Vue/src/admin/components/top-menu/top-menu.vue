<template>
    <v-toolbar color="blue darken-3" dense fixed clipped-left app dark>
        <v-toolbar-side-icon @click="drawerChanged"></v-toolbar-side-icon>
        <v-toolbar-title>
            <span class="title">Nucleus Vue</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon v-on:click.native="logOut">
            <v-icon>exit_to_app</v-icon>
        </v-btn>
    </v-toolbar>
</template>

<script src="./top-menu.ts"></script>