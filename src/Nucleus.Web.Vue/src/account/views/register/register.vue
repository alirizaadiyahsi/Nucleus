<template>
    <div v-if="!registerComplete" class="register-form">
        <form v-on:submit.prevent="onSubmit">
            <h2 class="text-center">Register</h2>
            <div v-for="error in errors">
                <div class="alert alert-danger" role="alert">
                    {{error.value}}
                </div>
            </div>
            <div class="form-group">
                <input type="text" v-model="username" class="form-control" placeholder="User name" required="required">
            </div>
            <div class="form-group">
                <input type="email" v-model="email" class="form-control" placeholder="E-Mail" required="required">
            </div>
            <div class="form-group">
                <input type="password" v-model="password" class="form-control" placeholder="Password" required="required">
            </div>
            <div class="form-group">
                <button type="submit" class="btn btn-primary btn-block">Register</button>
            </div>
        </form>
        <p class="text-center"><router-link to="/account/login">Login</router-link></p>
    </div>

    <div v-else class="register-form">
        <form v-on:submit.prevent="onSubmit">
            <h2 class="text-center">Register</h2>
            <div class="alert alert-success" role="alert">
                {{resultMessage}} Please <router-link to="/account/login">Login</router-link>.
            </div>
        </form>
    </div>
</template>

<script src="./register.ts"></script>
<style src="./register.css"></style>