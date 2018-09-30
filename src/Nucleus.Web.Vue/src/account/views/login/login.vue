<template>
    <div class="login-form">
        <form v-on:submit.prevent="onSubmit">
            <h2 class="text-center">Login</h2>
            <div v-for="error in errors">
                <div class="alert alert-danger" role="alert">
                    {{error.value}}
                </div>
            </div>
            <div class="form-group">
                <input type="text" v-model="username" class="form-control" placeholder="User name" required="required">
            </div>
            <div class="form-group">
                <input type="password" v-model="password" class="form-control" placeholder="Password" required="required">
            </div>
            <div class="form-group">
                <button type="submit" class="btn btn-primary btn-block">Login</button>
            </div>
        </form>
        <p class="text-center"><router-link to="/account/register">Register</router-link></p>
    </div>
</template>

<script src="./login.ts"></script>
<style src="./login.css"></style>