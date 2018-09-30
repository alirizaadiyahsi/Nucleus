<template>
    <div class="main-nav">
        <div class="navbar navbar-inverse">
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>
                        <router-link to="/admin/home" :exact="true">
                            <span class="glyphicon glyphicon-home"></span> Home
                        </router-link>
                    </li>
                    <li>
                        <router-link to="/admin/counter">
                            <span class="glyphicon glyphicon-education"></span> Counter
                        </router-link>
                    </li>
                    <li>
                        <router-link to="/admin/fetchdata">
                            <span class="glyphicon glyphicon-th-list"></span> Fetch data
                        </router-link>
                    </li>
                    <li>
                        <router-link to="/admin/user-list">
                            <span class="glyphicon glyphicon-user"></span> Users
                        </router-link>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</template>

<style src="./nav-menu.css"></style>
<script src="./nav-menu.ts"></script>