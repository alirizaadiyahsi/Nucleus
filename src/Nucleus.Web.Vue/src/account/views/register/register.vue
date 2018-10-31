<template>
    <div v-if="!registerComplete">
        <v-card class="elevation-12">
            <v-toolbar dark color="primary">
                <v-toolbar-title>Register</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <div v-for="error in errors" :key="error.name">
                    <v-alert :value="true" type="error">
                        {{error.value}}
                    </v-alert>
                </div>
                <v-form ref="form">
                    <v-text-field name="username" label="User name" type="text" v-model="username" :rules="[appConsts.validationRules.required]"></v-text-field>
                    <v-text-field name="email" label="E-mail address" type="text" v-model="email" :rules="[appConsts.validationRules.required,appConsts.validationRules.email]"></v-text-field>
                    <v-text-field name="password" label="Password" type="password" v-model="password" :rules="[appConsts.validationRules.required]"></v-text-field>
                </v-form>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn to="/account/login">Login</v-btn>
                <v-btn color="primary" @click="onSubmit">Register</v-btn>
            </v-card-actions>
        </v-card>
    </div>
    <div v-else>
        <v-card class="elevation-12">
            <v-toolbar dark color="primary">
                <v-toolbar-title>Registration successful</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <v-alert :value="true" type="success">
                    {{resultMessage}}
                </v-alert>
            </v-card-text>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn to="/account/login">Login</v-btn>
            </v-card-actions>
        </v-card>
    </div>
</template>

<script src="./register.ts"></script>
<style src="./register.css"></style>