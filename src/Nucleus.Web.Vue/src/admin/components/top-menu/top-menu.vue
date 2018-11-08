<template>
    <v-toolbar color="blue darken-3" dense fixed clipped-left app dark>
        <v-toolbar-side-icon @click="drawerChanged"></v-toolbar-side-icon>
        <v-toolbar-title>
            <span class="title">Nucleus Vue</span>
        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-menu>
            <v-toolbar-title slot="activator">
                <span>{{authStore.getTokenData().sub}}</span>
                <v-icon dark>arrow_drop_down</v-icon>
            </v-toolbar-title>

            <v-list>
                <v-list-tile @click="dialog=true">
                    <v-icon>lock</v-icon>
                    <v-list-tile-title>Change password</v-list-tile-title>
                </v-list-tile>
            </v-list>
        </v-menu>
        <v-btn icon v-on:click.native="logOut">
            <v-icon>exit_to_app</v-icon>
        </v-btn>

        <v-dialog v-model="dialog" max-width="500px">
            <v-card>
                <v-card-title>
                    <span class="headline">Change Password</span>
                </v-card-title>

                <v-card-text>
                    <div v-for="error in errors" :key="error.name">
                        <v-alert :value="true" type="error">
                            {{error.value}}
                        </v-alert>
                    </div>
                    <v-form ref="form">
                        <v-text-field v-model="changePasswordInput.currentPassword" type="password" autocomplete="current-password" label="Current password" :rules="[appConsts.validationRules.required]"></v-text-field>
                        <v-text-field v-model="changePasswordInput.newPassword" type="password" autocomplete="current-password" label="New password" :rules="[appConsts.validationRules.required]"></v-text-field>
                        <v-text-field v-model="changePasswordInput.passwordRepeat" type="password" autocomplete="current-password" label="Password repeat" :rules="[appConsts.validationRules.required]" :error-messages='passwordMatchError()'></v-text-field>
                    </v-form>
                </v-card-text>

                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" flat @click="dialog = false">Cancel</v-btn>
                    <v-btn color="blue darken-1" flat @click="save">Save</v-btn>
                </v-card-actions>
            </v-card>
        </v-dialog>
    </v-toolbar>
</template>

<script src="./top-menu.ts"></script>