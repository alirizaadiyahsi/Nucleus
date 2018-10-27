<template>
    <b-navbar toggleable="md" type="dark" variant="info">
        <b-navbar-toggle target="nav_collapse"></b-navbar-toggle>
        <router-link to="/admin/home" :exact="true" class="navbar-brand col-sm-3 col-md-2 mr-0">
            <span class="glyphicon glyphicon-home"></span> Nucleus Vue
        </router-link>
        <b-collapse is-nav id="nav_collapse">

            <b-navbar-nav class="ml-auto">
                <li class="nav-item">
                    <router-link to="#" v-on:click.native="logOut" active-class="active" class="nav-link">
                        <i class="fas fa-sign-out-alt"></i> Logout
                    </router-link>
                </li>
            </b-navbar-nav>

        </b-collapse>
    </b-navbar>
</template>

<style src="./top-menu.css"></style>
<script src="./top-menu.ts"></script>