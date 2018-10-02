<template>
    <nav class="col-sm-3 col-md-2 d-none d-md-block bg-light sidebar">
        <div class="sidebar-sticky">
            <ul class="nav flex-column">
                <li class="nav-item">
                    <router-link to="/admin/home" :exact="true" class="nav-link active">
                        <span class="glyphicon glyphicon-home"></span> Home
                    </router-link>
                </li>
                <li class="nav-item">
                    <router-link to="/admin/counter" class="nav-link">
                        <span class="glyphicon glyphicon-education"></span> Counter
                    </router-link>
                </li>
                <li class="nav-item">
                    <router-link to="/admin/fetchdata" class="nav-link">
                        <span class="glyphicon glyphicon-th-list"></span> Fetch data
                    </router-link>
                </li>
            </ul>

            <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                <span>Administration</span>
            </h6>
            <ul class="nav flex-column mb-2">
                <li class="nav-item">
                    <router-link to="/admin/user-list" class="nav-link">
                        <span class="glyphicon glyphicon-user"></span> Users
                    </router-link>
                </li>
            </ul>
        </div>
    </nav>

</template>

<style src="./nav-menu.css"></style>
<script src="./nav-menu.ts"></script>