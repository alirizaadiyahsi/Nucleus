<template>
    <nav class="col-sm-3 col-md-2 d-none d-md-block bg-light sidebar">
        <div class="sidebar-sticky">
            <ul class="nav flex-column">
                <li class="nav-item">
                    <router-link to="/admin/home" active-class="active" class="nav-link">
                        <i class="fas fa-home"></i> Home
                    </router-link>
                </li>
                <li class="nav-item">
                    <router-link to="/admin/counter" active-class="active" class="nav-link">
                        <i class="fas fa-calculator"></i>  Counter
                    </router-link>
                </li>
            </ul>

            <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                <span>Administration</span>
            </h6>
            <ul class="nav flex-column mb-2">
                <li class="nav-item">
                    <router-link to="/admin/user-list" active-class="active" class="nav-link">
                        <i class="fas fa-users"></i>  Users
                    </router-link>
                </li>
                <li class="nav-item">
                    <router-link to="/admin/role-list" active-class="active" class="nav-link">
                        <i class="fas fa-suitcase"></i>  Roles
                    </router-link>
                </li>
            </ul>
        </div>
    </nav>

</template>

<style src="./nav-menu.css"></style>
<script src="./nav-menu.ts"></script>