<template>
    <div>
        <top-menu-component />
        <div class="container-fluid">
            <div class="row">
                <nav-menu-component />
                <div class="col-sm-9 col-md-10 mt-3">
                    <router-view></router-view>
                </div>
            </div>
        </div>
    </div>
</template>

<script src="./admin-layout.ts"></script>