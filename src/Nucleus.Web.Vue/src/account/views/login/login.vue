<template>
    <v-card class="elevation-12">
        <v-toolbar dark color="primary">
            <v-toolbar-title>Login</v-toolbar-title>
        </v-toolbar>
        <v-card-text>
            <div v-for="error in errors" :key="error.name">
                <v-alert :value="true" type="error">
                    {{error.value}}
                </v-alert>
            </div>
            <v-form ref="form">
                <v-text-field prepend-icon="person" name="usernameoremail" label="User name or e-mail address" type="text" v-model="usernameoremail" :rules="[appConsts.validationRules.required]"></v-text-field>
                <v-text-field prepend-icon="lock" name="password" label="Password" type="password" v-model="password" :rules="[appConsts.validationRules.required]"></v-text-field>
            </v-form>
        </v-card-text>
        <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn to="/account/register">Register</v-btn>
            <v-btn color="primary" @click="onSubmit">Login</v-btn>
        </v-card-actions>
    </v-card>
</template>

<script src="./login.ts"></script>
<style src="./login.css"></style>