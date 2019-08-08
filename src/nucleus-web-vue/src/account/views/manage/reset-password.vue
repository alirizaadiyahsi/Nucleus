<template>
    <div v-if="!isPasswordReset">
        <v-card class="elevation-12">
            <v-toolbar dark color="primary">
                <v-toolbar-title>{{$t('ResetPassword')}}</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <div v-for="error in errors" :key="error.name">
                    <v-alert :value="true" type="error">
                        {{$t(error.name)}}
                    </v-alert>
                </div>
                <v-form ref="form" @keyup.native.enter="onSubmit">
                    <v-text-field prepend-icon="mdi-account" name="userNameOrEmail" type="text"
                                  :label="$t('UserNameOrEmailAddress')"
                                  v-model="resetPasswordInput.userNameOrEmail"
                                  :rules="[requiredError]"></v-text-field>
                    <v-text-field prepend-icon="mdi-lock" name="password" type="password"
                                  :label="$t('Password')"
                                  v-model="resetPasswordInput.password"
                                  :rules="[requiredError]"></v-text-field>
                    <v-text-field prepend-icon="mdi-repeat" name="passwordRepeat" type="password" :label="$t('PasswordRepeat')"
                                  v-model="resetPasswordInput.passwordRepeat"
                                  :rules="[requiredError]"
                                  :error-messages="passwordMatchError(resetPasswordInput.password,resetPasswordInput.passwordRepeat)"></v-text-field>

                </v-form>
            </v-card-text>
            <v-card-actions class="pa-5">
                <v-spacer></v-spacer>
                <v-btn color="primary" text to="/account/login">{{$t('Login')}}</v-btn>
                <v-btn color="primary" @click="onSubmit">{{$t('ResetPassword')}}</v-btn>
            </v-card-actions>
        </v-card>
    </div>
    <div v-else>
        <v-card class="elevation-12">
            <v-toolbar dark color="primary">
                <v-toolbar-title>{{$t('ResetPassword')}}</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <v-alert :value="true" type="success">
                    {{resultMessage}}
                </v-alert>
            </v-card-text>
            <v-card-actions class="pa-5">
                <v-spacer></v-spacer>
                <v-btn color="primary" text to="/account/login">{{$t('Login')}}</v-btn>
            </v-card-actions>
        </v-card>
    </div>
</template>

<script src="./reset-password.ts"></script>