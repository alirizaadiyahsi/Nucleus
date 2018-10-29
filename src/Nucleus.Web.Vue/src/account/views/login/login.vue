<template>
    <div class="row justify-content-center">
        <div class="col-sm-8 col-md-6 col-lg-4">
            <div class="login-form">
                <form v-on:submit.prevent="onSubmit">
                    <h2 class="text-center">Login</h2>
                    <div v-for="error in errors" :key="error.name">
                        <div class="alert alert-danger" role="alert">
                            {{error.value}}
                        </div>
                    </div>
                    <div class="form-group">
                        <input type="text" v-model="usernameoremail" class="form-control" placeholder="User name or e-mail" required>
                    </div>
                    <div class="form-group">
                        <input type="password" v-model="password" class="form-control" placeholder="Password" required>
                    </div>
                    <div class="form-group">
                        <button type="submit" class="btn btn-primary btn-block">Login</button>
                    </div>
                    <p class="text-center">If you don't have an account please <router-link to="/account/register">register</router-link></p>
                </form>
            </div>
        </div>
    </div>
</template>

<script src="./login.ts"></script>
<style src="./login.css"></style>