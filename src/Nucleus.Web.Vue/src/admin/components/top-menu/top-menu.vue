<template>

    <nav class="navbar navbar-expand-lg navbar-dark bg-dark">
        <router-link to="/admin/home" :exact="true" class="navbar-brand col-sm-3 col-md-2 mr-0">
            <span class="glyphicon glyphicon-home"></span> Application Name
        </router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor03" aria-controls="navbarColor03" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarColor03">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <a class="nav-link" href="#">Home <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Features</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Pricing</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">About</a>
                </li>
                <li>
                    <router-link to="/" v-on:click.native="logOut" class="nav-link">
                        Logout
                    </router-link>
                </li>
            </ul>
            <form class="form-inline">
                <input class="form-control mr-sm-2" type="search" placeholder="Search" aria-label="Search">
                <button class="btn btn-outline-primary my-2 my-sm-0" type="submit">Search</button>
            </form>
        </div>
    </nav>
</template>

<style src="./top-menu.css"></style>
<script src="./top-menu.ts"></script>