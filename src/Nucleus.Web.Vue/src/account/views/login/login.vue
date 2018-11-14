<template>
    <v-card class="elevation-12" @keyup.enter="onSubmit">
        <v-toolbar dark color="primary">
            <v-toolbar-title>{{$t('Login')}}</v-toolbar-title>
        </v-toolbar>
        <v-card-text>
            <div v-for="error in errors" :key="error.name">
                <v-alert :value="true" type="error">
                    {{error.value}}
                </v-alert>
            </div>
            <v-form ref="form">
                <v-text-field prepend-icon="person" name="userNameOrEmail" :label="$t('UserNameOrEmailAddress')" type="text" v-model="loginInput.userNameOrEmail" :rules="[appConsts.validationRules.required]"></v-text-field>
                <v-text-field prepend-icon="lock" name="password" :label="$t('Password')" type="password" v-model="loginInput.password" :rules="[appConsts.validationRules.required]"></v-text-field>
            </v-form>
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn to="/account/register">{{$t('Register')}}</v-btn>
            <v-btn color="primary" @click="onSubmit">{{$t('Login')}}</v-btn>
        </v-card-actions>
    </v-card>
</template>

<script src="./login.ts"></script>
<style src="./login.css"></style>