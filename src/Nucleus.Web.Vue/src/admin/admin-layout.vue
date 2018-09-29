<template>
    <div>
        <top-menu-component />
        <div class="container-fluid">
            <div class="row">
                <div class="col-sm-3">
                    <nav-menu-component />
                </div>
                <div class="col-sm-9">
                    <router-view></router-view>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="./admin-layout.ts"></script>