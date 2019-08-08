<template>
    <v-dialog v-model="dialog" max-width="500px">
        <v-card>
            <v-card-title>
                <span class="headline">{{$t('ChangePassword')}}</span>
            </v-card-title>

            <v-card-text>
                <div v-for="error in errors" :key="error.name">
                    <v-alert :value="true" type="error">
                        {{$t(error.name)}}
                    </v-alert>
                </div>
                <v-form ref="form">
                    <v-text-field name="currentPassword" type="password" :label="$t('CurrentPassword')"
                                  v-model="changePasswordInput.currentPassword"
                                  :rules="[requiredError]"></v-text-field>
                    <v-text-field name="newPassword" type="password" :label="$t('NewPassword')"
                                  v-model="changePasswordInput.newPassword"
                                  :rules="[requiredError]"></v-text-field>
                    <v-text-field name="passwordRepeat" type="password" :label="$t('PasswordRepeat')"
                                  v-model="changePasswordInput.passwordRepeat"
                                  :rules="[requiredError]"
                                  :error-messages="passwordMatchError(changePasswordInput.newPassword,changePasswordInput.passwordRepeat)"></v-text-field>
                </v-form>
            </v-card-text>

            <v-card-actions class="pa-5">
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="dialog = false">{{$t('Cancel')}}</v-btn>
                <v-btn color="blue darken-1" text @click="save">{{$t('Save')}}</v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script src="./change-password.ts"></script>