<template>
    <div class="row justify-content-center">
        <div class="col-sm-8 col-md-6 col-lg-4">
            <div v-if="!registerComplete" class="register-form">
                <form v-on:submit.prevent="onSubmit">
                    <h2 class="text-center">Register</h2>
                    <div v-for="error in errors" :key="error.name">
                        <div class="alert alert-danger" role="alert">
                            {{error.value}}
                        </div>
                    </div>
                    <div class="form-group">
                        <input type="text" v-model="username" class="form-control" placeholder="User name" required>
                    </div>
                    <div class="form-group">
                        <input type="email" v-model="email" class="form-control" placeholder="E-Mail" required>
                    </div>
                    <div class="form-group">
                        <input type="password" v-model="password" class="form-control" placeholder="Password" required>
                    </div>
                    <div class="form-group">
                        <button type="submit" class="btn btn-primary btn-block">Register</button>
                    </div>
                    <p class="text-center">If you already have an account please <router-link to="/account/login">login</router-link> to continue</p>
                </form>
            </div>

            <div v-else class="register-form">
                <form v-on:submit.prevent="onSubmit">
                    <h2 class="text-center">Register</h2>
                    <div class="alert alert-success" role="alert">
                        {{resultMessage}} Please <router-link to="/account/login">Login</router-link>.
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script src="./register.ts"></script>
<style src="./register.css"></style>