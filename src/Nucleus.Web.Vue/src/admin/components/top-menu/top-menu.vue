<template>

    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <router-link to="/admin/home" :exact="true" class="navbar-brand col-sm-3 col-md-2 mr-0">
            <span class="glyphicon glyphicon-home"></span> Application Name
        </router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor03" aria-controls="navbarColor03" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse">
            <ul class="navbar-nav ml-auto">
                <li class="nav-item">
                    <router-link to="#" v-on:click.native="logOut" active-class="active" class="nav-link">
                        <i class="fas fa-sign-out-alt"></i> Logout
                    </router-link>
                </li>
            </ul>
        </div>
    </nav>
</template>

<style src="./top-menu.css"></style>
<script src="./top-menu.ts"></script>