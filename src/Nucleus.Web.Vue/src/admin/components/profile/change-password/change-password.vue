<template>
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
                    <input name="userName" type="text" value="username" autocomplete="username" v-show="false" />
                    <v-text-field name="currentPassword" v-model="changePasswordInput.currentPassword" type="password" autocomplete="current-password" label="Current password" :rules="[appConsts.validationRules.required]"></v-text-field>
                    <v-text-field name="newPassword" v-model="changePasswordInput.newPassword" type="password" autocomplete="new-password" label="New password" :rules="[appConsts.validationRules.required]"></v-text-field>
                    <v-text-field name="passwordRepeat" v-model="changePasswordInput.passwordRepeat" type="password" autocomplete="new-password" label="Password repeat" :rules="[appConsts.validationRules.required]" :error-messages='passwordMatchError()'></v-text-field>
                </v-form>
            </v-card-text>

            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" flat @click="dialog = false">Cancel</v-btn>
                <v-btn color="blue darken-1" flat @click="save">Save</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script src="./change-password.ts"></script>