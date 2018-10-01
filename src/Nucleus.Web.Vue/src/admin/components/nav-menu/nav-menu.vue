<template>
    <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
        <router-link to="/admin/home" :exact="true" class="nav-link active">
            <span class="glyphicon glyphicon-home"></span> Home
        </router-link>

        <router-link to="/admin/counter" class="nav-link">
            <span class="glyphicon glyphicon-education"></span> Counter
        </router-link>

        <router-link to="/admin/fetchdata" class="nav-link">
            <span class="glyphicon glyphicon-th-list"></span> Fetch data
        </router-link>

        <router-link to="/admin/user-list" class="nav-link">
            <span class="glyphicon glyphicon-user"></span> Users
        </router-link>
    </div>
</template>

<style src="./nav-menu.css"></style>
<script src="./nav-menu.ts"></script>