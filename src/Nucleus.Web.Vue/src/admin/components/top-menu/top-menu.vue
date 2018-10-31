<template>
    <v-toolbar color="red" dense fixed clipped-left app>
        <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
        <v-toolbar-title>
            <span class="title">Nucleus Vue</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon v-on:click.native="logOut">
            <v-icon>exit_to_app</v-icon>
        </v-btn>
    </v-toolbar>
</template>

<style src="./top-menu.css"></style>
<script src="./top-menu.ts"></script>