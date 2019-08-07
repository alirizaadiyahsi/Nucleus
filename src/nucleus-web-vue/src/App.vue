<template>
    <v-app>
        <router-view></router-view>
        <v-layout row justify-center>
            <v-dialog v-model="nucleus.isLoading" persistent content content-class="centered-dialog">
                <v-container fill-height>
                    <v-layout column justify-center align-center>
                        <v-progress-circular indeterminate :size="70" :width="7" :color="'primary'"></v-progress-circular>
                        <h1>Loading...</h1>
                    </v-layout>
                </v-container>
            </v-dialog>
        </v-layout>
    </v-app>
</template>

<script src="./App.ts"></script>